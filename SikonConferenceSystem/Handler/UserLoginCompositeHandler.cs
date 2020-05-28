﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using ModelLibrary;
using Newtonsoft.Json;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Model;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.Handler
{
    public class UserLoginCompositeHandler
    {
        private IUserLoginMenu _userLoginVm;
        private string _previousLoginId;

        public UserLoginCompositeHandler(IUserLoginMenu userLoginVm)
        {
            _userLoginVm = userLoginVm;
        }

        /*
        TODO: Reading(probably posting too) from the database with strings is not case sensitive, this is potentially very annoying. Can be fixed easily in terms of logging in, but could be major in terms of signing up.
        It means that a user with the email luckyluke@gmail.com could be unable to sign up if someone already has signed up with LuckyLuke@gmail.com as their mail.
        This is assumed related to the SQL statements generated by DBUtility, not the lib. itself but execution of the statements in SQL. 
        */
        public async void Login()
        {
            UserLoginMenuVM Vm = (UserLoginMenuVM)_userLoginVm;
            Vm.IsLoadingUser = true;
            Vm.WrongLogin = false;

            if (Vm.LoginId != _previousLoginId)
            {
                var consumer = CommonConsumerFactory.Create(new ConsumerStringIds<TupleJSON>());
                TupleJSON loginDetails =
                    await consumer.GetOneAsync(new[] { Vm.LoginId });


                if (loginDetails != null)
                    Vm.LoadedUser = GetUser(loginDetails);
            }

            if (Vm.Password == Vm.LoadedUser?.Password)
            {
                Vm.NavigationService?.Navigate(Vm.NavigationService.UserLoginProfileMenu, Vm.LoadedUser);
            }
            else
            {
                Vm.WrongLogin = true;
            }

            _previousLoginId = Vm.LoginId;
            Vm.IsLoadingUser = false;
        }

        private User GetUser(TupleJSON loginDetails) //The exact type is important for deserializing correctly.
        {
            if (loginDetails.GetItem<User>(1) is User user)
            {
                return user;
            }
            if (loginDetails.GetItem<Speaker>(2) is Speaker speaker)
            {
                return speaker;
            }

            return loginDetails.GetItem<Admin>(3);

        }

        public async void SignUp(User newUser, Action onErrors)
        {
            IUserLoginMenu Vm = _userLoginVm;
            Vm.LoadedUser.Email = string.Empty;
            Vm.LoadedUser.PhoneNumber = string.Empty;

            var loginConsumer = CommonConsumerFactory.Create(new ConsumerStringIds<TupleJSON>());

            var userConsumer = CommonConsumerFactory.Create(new Consumer<User>());

            bool postOk = await userConsumer.PostAsync(newUser);
            if (postOk)
            {
                //Vm.LoadedUser = newUser;
                NavigationService navigationService = NavigationService.GetService(Contents.UserLoginContent);
                navigationService.Navigate(navigationService.UserLoginProfileMenu, await RetrieveNewUser());
            }
            else if (await CheckNoDuplicates())
            {
                throw new ArgumentException("Post Failed");
            }



            async Task<bool> CheckNoDuplicates()
            {
                TupleJSON mailTuple = await loginConsumer.GetOneAsync(new[] { newUser.Email });
                TupleJSON phoneTuple = await loginConsumer.GetOneAsync(new[] { newUser.PhoneNumber });

                if (mailTuple != null)
                {
                    Vm.LoadedUser.Email = GetUser(mailTuple).Email;
                }
                if (phoneTuple != null)
                {
                    Vm.LoadedUser.PhoneNumber = GetUser(phoneTuple).PhoneNumber;
                }
                onErrors();
                return string.IsNullOrEmpty(Vm.LoadedUser.Email) && string.IsNullOrEmpty(Vm.LoadedUser.PhoneNumber);
            }

            async Task<User> RetrieveNewUser()
            {
                if (!string.IsNullOrEmpty(newUser.Email))
                {
                    TupleJSON response = await loginConsumer.GetOneAsync(new[] { newUser.Email });
                    return GetUser(response);
                }
                else
                {
                    TupleJSON response = await loginConsumer.GetOneAsync(new[] { newUser.PhoneNumber });
                    return GetUser(response);
                }
            }
        }

        public void SignOut()
        {
            if (!AppData.TryLogOut())
            {
                throw new Exception("Unable to log out");
            }
        }
    }
}
