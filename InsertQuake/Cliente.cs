using System;
using System.Collections.Generic;
using System.Text;

namespace InsertQuake
{
    public class Cliente
    {
        public int IdPosicao { get; set; }
        public string NickName { get; set; }
        public string Personagem { get; set; }

        public Cliente (int IdPosicao, string NickName, string Personagem)
        {

            this.IdPosicao = IdPosicao;
            this.NickName = NickName;
            this.Personagem = Personagem;


        }







    }
}
