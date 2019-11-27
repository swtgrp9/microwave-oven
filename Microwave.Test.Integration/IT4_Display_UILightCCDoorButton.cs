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
    public class IT4_Display_UILightCCDoorButton
    {

        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private ICookController _cookController;

        
        private IPowerTube _powerTube;

        private ITimer _timer;

        private IDoor _door;

        private IOutput _output;

        private IUserInterface _userInterface;

        private ILight _light;

        private Display _iut;

        [SetUp]
        public void SetUp()
        {
            //fakes

            _output = Substitute.For<IOutput>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();

            //includes
            _cookController = new CookController(_timer, _iut, _powerTube, _userInterface);
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _iut, _light, _cookController);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            //testing
            _iut = new Display(_output);

        }
    }
}
