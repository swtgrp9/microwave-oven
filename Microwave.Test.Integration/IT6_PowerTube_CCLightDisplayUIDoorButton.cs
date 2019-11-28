using System;
using System.Runtime.InteropServices;
using Castle.Core.Smtp;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    public class IT6_PowerTube_CCLightDisplayUIDoorButton
    {
        //Top
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;

        //stubs

        private IOutput _output;

        //Includes
        private CookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private UserInterface _iut;

        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            //stubs
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();


            //top
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();




            //includes
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);


            //testing
            _cookController = new CookController(_timer, _display, _powerTube);
            _iut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _iut;

        }
        [Test]
        public void StartCooking_CookControllerCallsPowerTube_TurnOn()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is("PowerTube works with 50 W"));
        }

        [Test]
        public void StartCooking_CookControllerCallsPowerTube_TurnOff()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is("PowerTube turned off"));
        }

        [Test]
        public void StartCooking_DoorOpen_Powertube_Off()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();
            //_door.Opened += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is("PowerTube turned off"));
        }

        [Test]
        public void Press_Cancel_Powertube_off()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            //_timer.Expired += Raise.Event();

            _output.Received(1).OutputLine(Arg.Is("PowerTube turned off"));
        }
    }
}
