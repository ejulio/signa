;(function(window, View, Signa, undefined)
{
    function UserHands(cameraFactory, container, leapController, width, height)
    {
        var userHandmodelScene = new Signa.scene.Scene(cameraFactory, container, width, height);

        this._userRiggedHand = new Signa.scene.RiggedHandScene(leapController, userHandmodelScene);
    }

    UserHands.prototype = {
        _userRiggedHand: undefined,

        onNewSign: function(signInfo)
        {
            
        },

        onRecognize: function()
        {

        }
    };

    View.index.UserHands = UserHands;
})(window, window.View, window.Signa);
