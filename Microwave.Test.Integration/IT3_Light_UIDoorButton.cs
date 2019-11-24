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
    public class IT3_Light_UIDoorButton
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private ICookController _cookController;
        
        private IDisplay _display;

        private IPowerTube _powerTube;

        private ITimer _timer;

        private IDoor _door;
        
        private IOutput _output;

        private IUserInterface _userInterface;

        private Light _uut;

        [SetUp]
        public void SetUp()
        {
            //fakes

            _output = Substitute.For<IOutput>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();

            //includes
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);
            _display = new Display(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _uut, _cookController);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();


            //testing
            _uut = new Light(_output);

        }

        [Test]
        public void TurnOn_DisplayOutput()
        {

        }

        [Test]
        public void TurnOff_DisplayNoOutput()
        {

        }
    }

}
