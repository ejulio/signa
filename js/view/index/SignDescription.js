;(function(window, View, Signa, undefined)
{
    'use strict';
    
    function SignDescription(textContainer)
    {
        this._textContainer = textContainer;
    }

    SignDescription.prototype = {
        _textContainer: undefined,

        onNewSign: function(signInfo)
        {
            this._textContainer
                .text(signInfo.Description);
        },

        onRecognize: function()
        {
        }
    };

    View.index.SignDescription = SignDescription;
})(window, window.View, window.Signa);
