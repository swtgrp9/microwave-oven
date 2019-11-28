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
            _door = Substitute.For<IDoor>();

            //includes
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            //testing
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

        }

        [Test]
        public void PwrButPressed_DisplayPower()
        {
            

            _powerButton.Press();
            _display.Received(1).ShowPower(Arg.Any<int>());

        }


        [Test]
        public void TimerButPressed_DisplayDefaultTimer()
        {
            _powerButton.Press();
            _timeButton.Press();
            _display.Received().ShowTime(Arg.Any<int>(), Arg.Any<int>());

        }

        [Test]
        public void StartCancelButtonPressed_StartCooking()
        {
            _startCancelButton.Press();
            _cookController.StartCooking(Arg.Any<int>(), Arg.Any<int>());
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
            _powerButton.Press();
            _display.ShowPower(Arg.Any<int>());
            _timeButton.Press();
            _display.ShowTime(Arg.Any<int>(), Arg.Any<int>());
            
        }

        [Test]
        public void PwrButPressed_WhileChoosingTime_DisplayPower()
        {
            _timeButton.Press();
            _display.ShowTime(Arg.Any<int>(), Arg.Any<int>());
            _powerButton.Press();
            _display.ShowPower(Arg.Any<int>());
        }

        [Test]
        public void StartCancelPressed_WhileSetup_ClearDisplayAndTurnOnLight()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            
            _light.Received(1).TurnOn();
            _cookController.Received(1).StartCooking(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void StartCancelPressed_WhileCooking()
        {
            _powerButton.Press();
            _timeButton.Press();
           
            _startCancelButton.Press();
            _cookController.Received(1).StartCooking(Arg.Any<int>(), Arg.Any<int>());
            
            _startCancelButton.Press();
            _cookController.Received(1).Stop();
            _display.Received(1).Clear();
            _light.Received(1).TurnOff();
           
           
        }

    }
}