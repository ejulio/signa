;(function(window, View, Signa, undefined)
{
    'use strict';

    function ContainerComDescricaoDoSinal(textContainer)
    {
        this._container = textContainer;
    }

    ContainerComDescricaoDoSinal.prototype = {
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

    View.index.ContainerComDescricaoDoSinal = ContainerComDescricaoDoSinal;
})(window, window.View, window.Signa);
