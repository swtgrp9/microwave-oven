using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    public class IT4_Display_UILightCCDoorButton
    {

        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private ICookController _cookController;


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
            _cookController = Substitute.For<ICookController>();

            //includes

            _light = new Light(_output);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            
            _iut = new Display(_output);

            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _iut, _light, _cookController);
        }

        [Test]

        public void Display_PowerTest()
        {
            _powerButton.Press();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 50 W"))); //Default power level er 50
        }

        [Test]
        public void Display_TimerTest()
        {
            _powerButton.Press();
            _timeButton.Press();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 01:00"))); //Default time er 1min
        }

        [Test]
        public void Display_CookingisDone_Clear_Test()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _userInterface.CookingIsDone();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));

        }

    }
}

