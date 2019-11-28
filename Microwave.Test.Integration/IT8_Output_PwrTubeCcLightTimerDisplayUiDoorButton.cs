using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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

        private StringWriter stringwriter;

        [SetUp]
        public void SetUp()
        {

            stringwriter = new StringWriter();
            Console.SetOut(stringwriter);

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


        #region Light-Output

        [Test]
        public void Test_TurnOnLight_ReadyState()
        {
            _door.Open();


            Assert.That(stringwriter.ToString(), Does.StartWith("Light is turned on"));

        }

        [Test]
        public void Test_TurnOffLight_DoorIsOpenState()
        {
            _door.Open();

            //Redundant - man kunne skrives contain istedet for StartWith
            //StringBuilder sb = stringwriter.GetStringBuilder(); //Clear stringwriter så der ikke står "light is turned on" fra når døren åbnes
            //sb.Remove(0, sb.Length);

            _door.Close();

            Assert.That(stringwriter.ToString(), Does.Contain("Light is turned off"));
        }

        #endregion


        #region Display-Output

        [Test]
        public void Test_ShowPower_ReadyState()
        {
            _powerButton.Press();

            Assert.That(stringwriter.ToString(), Does.StartWith("Display shows: 50 W"));
        }

        [Test]
        public void Test_ShowTime_SetPowerState()
        {
            _powerButton.Press();
            _timeButton.Press();

            Assert.That(stringwriter.ToString(), Does.Contain("Display shows: 01:00"));

        }

        #endregion

    }
}

