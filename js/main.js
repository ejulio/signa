var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    defaultCameraFactory = new Signa.DefaultCameraFactory(width / height);

$('#play-pause').click(function()
{
    window.riggedHandPlayer.toggle();
});

var id = 0;

$('#save').click(function()
{
    signalRecognizer.save(id++);
});

$('#train').click(function()
{
    signalRecognizer.train();
    signalRecognizer.setSignalToRecognizeId(signalId);
});

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

riggedHandPlayer.loadRecording('recordings/test-a.json');

var signalId = 0;
signalRecognizer.setSignalToRecognizeId(signalId);
signalRecognizer.addRecognizeEventListener(function()
{
    signalId = (signalId + 1) % 3;
    signalRecognizer.setSignalToRecognizeId(signalId);
    console.log('Sinal reconhecido. Sigal atual: ' + signalId);
    // sequencia de sinais: 0: mão aberta, 1: mão fechada, 2: paz e amor
});
