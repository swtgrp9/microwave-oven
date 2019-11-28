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
    public class IT5_CookController_LightDisplayUIDoorButton
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

            //_light = new Light(_output);
            _light = Substitute.For<ILight>();


            //includes
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);

           
            //Sut
           
            _cookController = new CookController(_timer, _display, _powerTube);
            _iut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _iut;

    


        }
        [Test]
        public void UserInterfaceCalledByCookController()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.Event();
            _light.Received(1).TurnOff();
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
        public void bla()
        {

        }
    }
}
