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
    public class IT7_Timer_PwrTubeCcLightDisplayUiDoorButton
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDisplay _display;

        private IPowerTube _powerTube;

        private ICookController _cookController;

        
        private IDoor _door;

        private IOutput _output;

        private IUserInterface _userInterface;

        private ILight _light;

        private Timer _uut;

        [SetUp]
        public void SetUp()
        {
            //fakes

            _output = Substitute.For<IOutput>();
          
            //includes
            _display = new Display(_output);
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            


            //testing
            _uut = new Timer();

        }
    }
}
