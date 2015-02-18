var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    container = $("#handmodel-user"),
    cameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
    leapController = new Leap.Controller();

cameraFactory = new Signa.camera.OrbitControlsCameraFactory(cameraFactory),

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
    var hand = frame.hands[0];
    var anglesBetweenFingers = [];
    var length = hand.fingers.length - 1;
    for (var i = 0; i < length; i++)
    {
        var origin = new THREE.Vector3();
        var destiny = new THREE.Vector3();

        origin.fromArray(hand.fingers[i].tipPosition);
        destiny.fromArray(hand.fingers[i + 1].tipPosition);

        anglesBetweenFingers.push(origin.angleTo(destiny));
    }

    var signalParameters = {
        palmNormal: hand.palmNormal,
        handDirection: hand.direction,
        anglesBetweenFingers: anglesBetweenFingers
    };
    Signa.HUB.saveSignSample($('#description').val(), json, signalParameters);
});
