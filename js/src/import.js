var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    container = $("#handmodel-user"),
    cameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
    leapController = new Leap.Controller();

cameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory);

Signa.initHubs();

var userHandmodelScene = new Signa.scene.Scene(cameraFactory, container, width, height);

userHandmodelScene = new Signa.scene.RiggedHandScene(leapController, userHandmodelScene);

userHandmodelScene.render();

userHandmodelScene = new Signa.scene.PlaybackRiggedHandScene(leapController);

var serverFrames, json;

$('#sign-file').change(function(event)
{
    var file = event.target.files[0],
        fileReader = new FileReader();

    fileReader.onload = function(event)
    {
        var result = event.target.result,
            player = userHandmodelScene._player,
            recording = new player.Recording();

        json = result;

        recording.readFileData(JSON.parse(result), function(frames)
        {
            serverFrames = frames;
            recording.setFrames(frames);
        });

        userHandmodelScene._player.setRecording(recording);
    };

    fileReader.readAsText(file);
});

$('#save').click(function()
{
    var frame = new Leap.Frame(serverFrames[0]);
    var data = new Signa.recognizer.FrameSignDataProcessor().process(frame);
    Signa.signalrHub().saveSignSample($('#description').val(), json, data);
});
