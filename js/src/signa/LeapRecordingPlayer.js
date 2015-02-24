;(function(window, Signa, undefined)
{
    'use strict';
    
    function LeapRecordingPlayer(leapController)
    {
        leapController
            .use('playback', {
                overlay: false,
                loop: true,
                pauseOnHand: false
            });

        this._player = leapController.plugins.playback.player;
    }

    LeapRecordingPlayer.prototype = {
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
        },

        loadFrames: function(loadedFrames, callback)
        {
            var recording = new this._player.Recording({
                loop: true
            });

            recording.readFileData(loadedFrames, callback);

            this._player.setRecording(recording);
        }
    };

    Signa.LeapRecordingPlayer = LeapRecordingPlayer;
})(window, window.Signa);
