var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    defaultCameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
    appSignInfo;

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
            appSignInfo = signInfo;
            window.riggedHandPlayer.loadRecording('recordings/' + signInfo.ExampleFilePath);
        });
    });
}

function initializeExampleHandScene()
{
    var exampleHandleapController = new Leap.Controller(),
        container = $("#handmodel-example"),
        orbitConstrolsCameraFactory = new Signa.camera.OrbitControlsCameraFactory(defaultCameraFactory),
        exampleHandmodelScene = new Signa.scene.Scene(orbitConstrolsCameraFactory, container, width, height);

    orbitConstrolsCameraFactory.setSignaScene(exampleHandmodelScene);

    window.riggedHandPlayer = new Signa.scene.PlaybackRiggedHandScene(exampleHandleapController, exampleHandmodelScene);

    exampleHandleapController.on('playback.recordingSet', function()
    {
        signalRecognizer.setSignalToRecognizeId(appSignInfo.Id);
        $('#sign-description')
            .text(appSignInfo.Description)
            .addClass('signa-sign-word-error')
            .removeClass('signa-sign-word-success');

        console.log('carregou frames');
    });
}

function initializeUserHandScene(userHandLeapController)
{
    var container = $("#handmodel-user"),
        userHandmodelScene = new Signa.scene.Scene(defaultCameraFactory, container, width, height);

    window.userRiggedHand = new Signa.scene.RiggedHandScene(userHandLeapController, userHandmodelScene);
}

var signaLeapController = new Leap.Controller(),
    signalRecognizer = new Signa.recognizer.SignRecognizer(signaLeapController);

initializeExampleHandScene();
initializeUserHandScene(signaLeapController);

signalRecognizer.addRecognizeEventListener(function()
{
    $('#sign-description')
        .removeClass('signa-sign-word-error')
        .addClass('signa-sign-word-success');

    console.log('carregando próximo sinal');
    signalRecognizer.setSignalToRecognizeId(-1);

    window.setTimeout(function()
    {
        loadNextSignal();
    }, 500);
});

loadNextSignal();
