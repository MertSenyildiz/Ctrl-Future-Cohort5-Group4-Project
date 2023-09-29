﻿namespace Project.Core.Business
{
    public class BusinessRules
    {
        public static bool Run(params bool[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic)
                    return logic;
            }
            return true;
        }
    }
}
