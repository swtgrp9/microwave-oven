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
    public class IT8_Output_PwrTubeCcLightTimerDisplayUiDoorButton
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDisplay _display;

        private IPowerTube _powerTube;

        private ICookController _cookController;


        private IDoor _door;

        

        private IUserInterface _userInterface;

        private ILight _light;

        private Output _sut;

        [SetUp]
        public void SetUp()
        {
            //testing
            _sut = new Output();


            //includes
            _display = new Display(_sut);
            _light = new Light(_sut);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_sut);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

        }

        [Test]
        public void Testtesthest()
        {

        }
    }
}

