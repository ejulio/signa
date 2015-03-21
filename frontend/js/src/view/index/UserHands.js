;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function UserHands(cameraFactory, container, leapController, width, height)
    {
        var userHandmodelScene = new Signa.cena.Cena(cameraFactory, container, width, height);

        this._userRiggedHand = new Signa.cena.CenaComLeapRiggedHand(leapController, userHandmodelScene);

        this._container = container;
    }

    UserHands.prototype = {
        _userRiggedHand: undefined,
        _container: undefined,

        onNewSign: function(signInfo)
        {
            this._container
                .addClass('signa-handmodel-user-error')
                .removeClass('signa-handmodel-user-success');
        },

        onRecognize: function()
        {
            this._container
                .removeClass('signa-handmodel-user-error')
                .addClass('signa-handmodel-user-success');
        }
    };

    View.index.UserHands = UserHands;
})(window, window.View, window.Signa);
