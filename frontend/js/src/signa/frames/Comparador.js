;(function(window, Signa, undefined) {
    'use strict';

    var LIMITE_DIFERENCA_ENTRE_NUMEROS = 10;

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

            var dedosMaoA = maoA.Dedos;
            var dedosMaoB = maoB.Dedos;

            return this._dedosEstaoNaMesmaPosicao(dedosMaoA, dedosMaoB);
        },

        _dedosEstaoNaMesmaPosicao: function(dedosMaoA, dedosMaoB) {
            for (var i = 0; i < dedosMaoA.length; i++) {
                var posicaoDedoMaoA = dedosMaoA[i].PosicaoDaPonta;
                var posicaoDedoMaoB = dedosMaoB[i].PosicaoDaPonta;

                if (dedosMaoA[i].Tipo !== dedosMaoB[i].Tipo) {
                    console.log('DEDOS DE TIPOS DIFERENTES: ' + dedosMaoA[i].Tipo + ', ' + dedosMaoB[i].Tipo);
                }

                if (!arraysSaoIguais(posicaoDedoMaoA, posicaoDedoMaoB)) {
                    return false;
                }
            }
            return true;
        }
    };

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
        return diferenca < LIMITE_DIFERENCA_ENTRE_NUMEROS;
    }

    Signa.frames.Comparador = Comparador;
})(window, window.Signa);
