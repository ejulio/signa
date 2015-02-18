;(function(window, Signa, undefined)
{
    'use strict';
    
    function PlaybackRiggedHandScene(leapController)
    {
        leapController
            .use('playback', {
                overlay: false
            });

        this._player = leapController.plugins.playback.player;
    }

    PlaybackRiggedHandScene.prototype = {
        _player: undefined,

        loadRecording: function(url)
        {
            this._player.setRecording({
                url: url
            });
        },

        toggle: function()
        {
            this._player.toggle();
        }
    };

    Signa.scene.PlaybackRiggedHandScene = PlaybackRiggedHandScene;
})(window, window.Signa);
