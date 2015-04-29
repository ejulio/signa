;(function(window, View, Signa, undefined) {
    'use strict';
    
    function ContainerComMaosDoUsuario(cameraFactory, container, leapController, largura, altura) {
        this._container = container;

        Signa.cenas.CenaFactory.criarCenaComLeapRiggedHand(largura, altura, container, cameraFactory, leapController);
    }

    ContainerComMaosDoUsuario.prototype = {
        _container: undefined,

        onNovoSinal: function(signInfo) {
            this._container
                .addClass('signa-handmodel-user-error')
                .removeClass('signa-handmodel-user-success');
        },

        onReconhecer: function() {
            this._container
                .removeClass('signa-handmodel-user-error')
                .addClass('signa-handmodel-user-success');
        }
    };

    View.index.ContainerComMaosDoUsuario = ContainerComMaosDoUsuario;
})(window, window.View, window.Signa);
