using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

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

        private ITimer _timer;

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
            _timer = new Timer();
            _display = new Display(_sut);
            _light = new Light(_sut);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_sut);
            _cookController = new CookController(_timer, _display, _powerTube);
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

            //Redundant - man kunne skrive contain i stedet for StartWith
            //StringBuilder sb = stringwriter.GetStringBuilder(); //Clear stringwriter så der ikke står "light is turned on" fra når døren åbnes
            //sb.Remove(0, sb.Length);

            _door.Close();

            Assert.That(stringwriter.ToString(), Does.Contain("Light is turned off"));
        }

        [Test]
        public void Test_TurnOnLight_StartCooking() //Fejler?
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Assert.That(stringwriter.ToString(), Does.Contain("Light is turned on"));
        }

        [Test]
        public void Test_TurnOffLightCalled_onCookingIsDone()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press(); //Starter cooking

            _userInterface.CookingIsDone(); //Afslutter cooking

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

        [Test]
        public void Test_ShowTime_OnTimerTicks()
        {
            _cookController.StartCooking(50,60);

            Thread.Sleep(5000);

            Assert.That(stringwriter.ToString(), Does.Contain("Display shows: 00:57"));

        }

        [Test]
        public void Test_ClearCalled_onCookingIsDone()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();  //Starter cooking

            _userInterface.CookingIsDone(); //Afslutter cooking

            Assert.That(stringwriter.ToString(), Does.Contain("Display cleared"));

        }

        #endregion

        #region PowerTube-Output

        [Test]
        public void Test_TurnOnPowertube_CookingStart()
        {
            //_powerButton.Press();
            //_timeButton.Press();
            //_startCancelButton.Press();

            _cookController.StartCooking(50,1);

            Assert.That(stringwriter.ToString(), Does.StartWith("PowerTube works with 50 W"));

        }

        [Test]
        public void Test_TurnOffPowertube_OnTimerExpired()
        {
            _cookController.StartCooking(50,5); //Cooking kører i 5 sekunder

            Thread.Sleep(6000); //venter i 6 sekunder - Cooking er done herefter

            Assert.That(stringwriter.ToString(), Does.Contain("PowerTube turned off")); //Sikrer at powertube slukkes når cooking er done

        }

        [Test]
        public void Test_Extension_TurnOffPowertube_DoorOpenedDuringCooking()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();  //Starter cooking - sætter systemet i cooking state

            _door.Open();

            Assert.That(stringwriter.ToString(), Does.Contain("PowerTube turned off"));
        }

        [Test]
        public void Test_Extension_TurnOffPowertube_StartCancelButtonPressedDuringCooking()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();  //Starter cooking - sætter systemet i cooking state

            Thread.Sleep(2000);

            _startCancelButton.Press();

            Assert.That(stringwriter.ToString(), Does.Contain("PowerTube turned off"));
        }


        #endregion

    }
}

