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

        private Output _uut;

        [SetUp]
        public void SetUp()
        {
            //fakes

            
            //includes
            _display = new Display(_uut);
            _light = new Light(_uut);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_uut);



            //testing
            _uut = new Output();

        }
    }
}

