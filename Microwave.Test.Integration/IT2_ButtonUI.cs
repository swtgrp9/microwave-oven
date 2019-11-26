using System;
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
    public class IT1_ButtonUI
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

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

            //includes
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            //testing
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

        }

       
        [Test]
        public void TimerButPressed_DisplayDefaultTimer()
        {
            //_Display = off
            //Light = off
            //Cooking=false
            //Timer=Default
            //Door = closed

            _timeButton.Press();
            _display.ShowTime(00,00);

        }

        [Test]
        public void StartCancelButtonPressed_StartCooking()
        {
            _startCancelButton.Press();
            _cookController.StartCooking(50, 10);
        }

        [Test]
        public void StartCancelButtonPressed_StopCooking()
        {
            _startCancelButton.Press();
            _cookController.StartCooking(50, 10);

            _startCancelButton.Press();
            _cookController.Stop();
        }

        [Test]
        public void TmrButPressed_WhileChoosingPower_DisplayTimer()
        {
            _display.ShowPower(50);
            _timeButton.Press();
            _display.ShowTime(00,00);
            
        }



        public void StartCancelPressed_WhileSettingPower_ClearDisplay()
        {
            //Pressing Start/Cancel while choosing power:
            //Light off
            //ClearDisplay
        }

        public void StartCancelPressed_WhileSettingTimer_ClearDisplay()
        {
            //Pressing Start/Cancel while choosing power:
            //Light on
            //ClearDisplay
            //Start cooking
        }



    }
}