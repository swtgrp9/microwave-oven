﻿using System;
using System.Runtime.InteropServices;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_DoorUI
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IPowerTube _powerTube;

        private ICookController _cookController;
        private ILight _light;
        private IDisplay _display;
        private IDoor _door;
        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            //fakes
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();

            _powerTube = Substitute.For<IPowerTube>();

            //includes
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            //testing
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

        }

        [Test]
        public void OpenDoorTurnOnLight()
        {
            _door.Open();

            _light.Received(1).TurnOn();
        }

        [Test]
        public void OpenAndCloseDoorTurnOffLight()
        {
            _door.Open();
            _door.Close();

            _light.Received(1).TurnOff();
        }

        [Test]
        public void OpenDoorWhileSetup()
        {
            _powerButton.Press();
            _display.ShowPower(100);
            _door.Open();
           
            _display.Received().Clear();


        }

        //[Test]
        //public void OpenDoorWhileCooking()
        //{

        //    _powerTube.TurnOn(100);
        //    _cookController.StartCooking(100, 10);
        //    _door.Open();
        //    _powerTube.TurnOff();

        //}

        //[Test]
        //public void OpenDoorBeforeCooking()
        //{
        //    //_DisplayClear_LightOn_StopCooking_PauseTimer
        //    //LightOn
        //    //Cooking=false
        //    //Timer=Default


        //}

        //[Test]
        //public void OpenDoorWhileCooking()
        //{
        //    //_DisplayClear_LightOn_StopCooking_PauseTimer
        //    _door.Open();

        //    _light.Received(1).TurnOn();
        //}

        //[Test]
        //public void ClosedDoorBeforeCook()
        //{
        //    //_DisplayClear_LightOn_StopCooking_PauseTimer
        //}

        //[Test]
        //public void ClosedDoorDuringCook()
        //{
        //    //_DisplayClear_LightOn_StopCooking_PauseTimer
        //}



    }
}
