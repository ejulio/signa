;(function(window, View, Signa, undefined) {
    'use strict';

    function ContainerComDescricaoDoSinal(textContainer) {
        this._container = textContainer;
    }

    ContainerComDescricaoDoSinal.prototype = {
        _container: undefined,

        onNovoSinal: function(informacoesDoSinal) {
            this._container
                .text(informacoesDoSinal.Descricao);
        },

        onReconhecer: function() { }
    };

    View.index.ContainerComDescricaoDoSinal = ContainerComDescricaoDoSinal;
})(window, window.View, window.Signa);
