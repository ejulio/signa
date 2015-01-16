var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    defaultCameraFactory = new Signa.DefaultCameraFactory(width / height);

$('#play-pause').click(function()
{
    window.riggedHandPlayer.toggle();
});

function loadNextSignal()
{
    Signa.initHubs().done(function()
    {
        Signa.HUB.getNextSign(1).done(function(signInfo)
        {
            window.riggedHandPlayer.loadRecording('recordings/' + signInfo.ExampleFilePath);
            $('#sign-description').text(signInfo.Description);
            signalRecognizer.setSignalToRecognizeId(signInfo.Id);
        });
    });
}

function initializeExampleHandScene()
{
    var exampleHandleapController = new Leap.Controller(),
        container = $("#handmodel-example"),
        orbitConstrolsCameraFactory = new Signa.OrbitControlsCameraFactory(defaultCameraFactory),
        exampleHandmodelScene = new Signa.Scene(orbitConstrolsCameraFactory, container, width, height);

    orbitConstrolsCameraFactory.setSignaScene(exampleHandmodelScene);

    window.riggedHandPlayer = new Signa.PlaybackRiggedHandScene(exampleHandleapController, exampleHandmodelScene);
}

function initializeUserHandScene(userHandLeapController)
{
    var container = $("#handmodel-user"),
        userHandmodelScene = new Signa.Scene(defaultCameraFactory, container, width, height);

    window.userRiggedHand = new Signa.RiggedHandScene(userHandLeapController, userHandmodelScene);
}

var signaLeapController = new Leap.Controller(),
    signalRecognizer = new Signa.SignalRecognizer(signaLeapController);

initializeExampleHandScene();
initializeUserHandScene(signaLeapController);

signalRecognizer.addRecognizeEventListener(function()
{
    console.log('Sinal reconhecido');
});

loadNextSignal();
