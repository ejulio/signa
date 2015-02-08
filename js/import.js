var width = $("#handmodel-user").width(),
    height = $("#handmodel-user").height(),
    container = $("#handmodel-user"),
    defaultCameraFactory = new Signa.camera.DefaultCameraFactory(width / height),
    leapController = new Leap.Controller();

var userHandmodelScene = new Signa.scene.Scene(defaultCameraFactory, container, width, height);

userHandmodelScene = new Signa.scene.RiggedHandScene(leapController, userHandmodelScene);

userHandmodelScene.render();

userHandmodelScene = new Signa.scene.PlaybackRiggedHandScene(leapController);

$('#sign-file').change(function(event)
{
    var file = event.target.files[0],
        fileReader = new FileReader();

    fileReader.onload = function(event)
    {
        var result = event.target.result,
            json = JSON.parse(result),
            player = userHandmodelScene._player,
            recording = new player.Recording();

        recording.readFileData(json, function(frames)
        {
            recording.setFrames(frames);
        });

        userHandmodelScene._player.setRecording(recording);
    };

    fileReader.readAsText(file);
});
