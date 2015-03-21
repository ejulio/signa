;(function(window, Signa, undefined)
{
    'use strict';
    
    Signa.cenas.CenaFactory = {
        criarCenaComLeapRiggedHand: function(largura, altura, container, cameraFactory, leapController) {
            var cena = new Signa.cenas.Cena(cameraFactory, container, largura, altura);
            return new Signa.cenas.CenaComLeapRiggedHand(leapController, cena);
        }
    };
})(window, window.Signa);
