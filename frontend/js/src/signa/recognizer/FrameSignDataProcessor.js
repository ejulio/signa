;(function(global, Signa, undefined)
{
    'use strict';

    function FrameSignDataProcessor(){}
    FrameSignDataProcessor.prototype = {
        extrairFrameDaAmostra: function(frame)
        {
            return {
                MaoEsquerda: this._extrairDadosDaMaoEsquerda(frame.hands),
                MaoDireita: this._extrairDadosDaMaoDireita(frame.hands)
            };
        },

        _extrairDadosDaMaoEsquerda: function(maos)
        {
            if (this._ehMaoEsquerda(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoEsquerda(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoEsquerda: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'LEFT';
        },

        _extrairDadosDaMaoDireita: function(maos)
        {
            if (this._ehMaoDireita(maos[0]))
                return this._extrairDadosDaMao(maos[0]);

            if (this._ehMaoDireita(maos[1]))
                return this._extrairDadosDaMao(maos[1]);

            return null;
        },

        _ehMaoDireita: function(hand)
        {
            return hand && hand.type.toUpperCase() === 'RIGHT';
        },

        _extrairDadosDaMao: function(leapHand)
        {
            return {
                VetorNormalDaPalma: leapHand.palmNormal,
                Direcao: leapHand.direction,
                Dedos: this._extrairDadosDosDedos(leapHand.fingers)
            };
        },

        _extrairDadosDosDedos: function(leapFingers)
        {
            var dedos = new Array(leapFingers.length);

            for (var i = 0; i < dedos.length; i++)
            {
                dedos[i] = {
                    tipo: leapFingers[i].type,
                    direcao: leapFingers[i].direction
                };
            }

            return dedos;
        }
    };

    Signa.recognizer.FrameSignDataProcessor = FrameSignDataProcessor;
})(global = typeof global === 'undefined' ? window : global, global.Signa);
