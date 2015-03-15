;(function(window, View, Signa, undefined)
{
    'use strict';

    function SignDescription(textContainer)
    {
        this._container = textContainer;
    }

    SignDescription.prototype = {
        _container: undefined,

        onNewSign: function(informacoesDoSinal)
        {
            this._container
                .text(informacoesDoSinal.Descricao);
        },

        onRecognize: function()
        {
        }
    };

    View.index.SignDescription = SignDescription;
})(window, window.View, window.Signa);
