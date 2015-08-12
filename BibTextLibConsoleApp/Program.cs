﻿using BibTexLib;

namespace BibTextLibConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string sample = "%  a sample bibliography file% @article{small,author = { Freely, I.P.},title = { A small paper},journal = { The journal of small papers},year = 1997,volume = { -1},note = { to appear},}   @article{                big,author = { Jass, Hugh},title = { A big paper},journal = { The journal of big papers},year = 7991,volume = { MCMXCVII},}% The authors mentioned here are almost, but not quite,% entirely unrelated to Matt Groening.";

            BibTexParser parser = new BibTexParser();
            parser.Parse(sample);
        }
    }
}