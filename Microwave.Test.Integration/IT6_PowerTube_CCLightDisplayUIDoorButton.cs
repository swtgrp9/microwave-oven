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
    public class IT6_PowerTube_CCLightDisplayUIDoorButton
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDisplay _display;

        //private IPowerTube _powerTube;

        private ICookController _cookController;

        private ITimer _timer;

        private IDoor _door;

        private IOutput _output;

        private IUserInterface _userInterface;

        private ILight _light;

        private PowerTube _uut;

        [SetUp]
        public void SetUp()
        {
            //fakes

            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();

            //includes
            _display = new Display(_output);
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();


            //testing
            _uut = new PowerTube(_output);

        }
    }
}
