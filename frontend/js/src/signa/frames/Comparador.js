;(function(window, Signa, undefined) {
    'use strict';

    function arraysSaoIguais(arrayA, arrayB) {
        for (var i = 0; i < arrayA.length; i++) {
            if (!numerosSaoIguais(arrayA[i], arrayB[i])) {
                return false;
            }
        }
        return true;
    }

    function numerosSaoIguais(numeroA, numeroB) {
        var diferenca = Math.abs(numeroA - numeroB);
        //console.log(diferenca);
        return diferenca < 50;
    }

    function resultadosDasFuncoesSaoIguais(funcaoA, funcaoB) {
        //return funcaoA() === funcaoB();
        return true;
    }

    function Comparador() {}

    Comparador.prototype = {
        framesSaoIguais: function(frameA, frameB) {
            if (!frameA || !frameB) {
                return false;
            }

            return this._maosSaoIguais(frameA.MaoEsquerda, frameB.MaoEsquerda) &&
                this._maosSaoIguais(frameA.MaoDireita, frameB.MaoDireita);
        },

        _maosSaoIguais: function(maoA, maoB) {
            if (maoA === maoB) {
                return true;
            } else if (!maoA || !maoB) {
                return false;
            }

            var propriedadesArray = ['VetorNormalDaPalma', 'PosicaoDaPalma', 'VelocidadeDaPalma', 'Direcao'];
            var propriedadesNumericas = ['RaioDaEsfera', 'Pitch', 'Roll', 'Yaw'];

            return this._propriedadesSaoIguais(maoA, maoB, propriedadesArray, arraysSaoIguais) &&
                this._propriedadesSaoIguais(maoA, maoB, propriedadesNumericas, numerosSaoIguais);
        },

        _propriedadesSaoIguais: function(maoA, maoB, propriedades, igual) {
            for (var i = 0; i < propriedades.length; i++) {
                var propriedade = propriedades[i];
                if (!igual(maoA[propriedade], maoB[propriedade])) {
                    return false;
                }
            }
            return true;
        }
    };

    Signa.frames.Comparador = Comparador;
})(window, window.Signa);
