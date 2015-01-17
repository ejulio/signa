;(function(window, View, Signa, undefined)
{
    function SignDescription(textContainer)
    {
        this._textContainer = textContainer;
    }

    SignDescription.prototype = {
        _textContainer: undefined,

        onNewSign: function(signInfo)
        {
            this._textContainer
                .text(signInfo.Description)
                .addClass('signa-sign-word-error')
                .removeClass('signa-sign-word-success');
        },

        onRecognize: function()
        {
            this._textContainer
                .removeClass('signa-sign-word-error')
                .addClass('signa-sign-word-success');
        }
    };

    View.index.SignDescription = SignDescription;
})(window, window.View, window.Signa);
