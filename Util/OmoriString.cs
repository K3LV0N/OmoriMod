using System;

namespace OmoriMod.Util
{
    public static class OmoriString
    {
        private static String _string = OmoriMod.MOD_NAME;

        /// <summary>
        /// Creates a string that has <see cref="OmoriMod.MOD_NAME"/> attached to the front.
        /// Simply because I want to stop typing it out.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String str(String str)
        {
            return _string + str;
        }
    }
}
