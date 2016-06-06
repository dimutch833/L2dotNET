﻿using L2dotNET.GameService.Model.Player;
using L2dotNET.GameService.Network.Serverpackets;
using L2dotNET.GameService.Tables;

namespace L2dotNET.GameService.Network.Clientpackets.RecipeAPI
{
    class RequestRecipeBookDestroy : GameServerNetworkRequest
    {
        public RequestRecipeBookDestroy(GameClient client, byte[] data)
        {
            base.makeme(client, data);
        }

        private int _id;

        public override void read()
        {
            _id = readD();
        }

        public override void run()
        {
            L2Player player = Client.CurrentPlayer;

            if (player._recipeBook == null)
            {
                player.sendSystemMessage(SystemMessage.SystemMessageId.RECIPE_INCORRECT);
                player.sendActionFailed();
                return;
            }

            L2Recipe rec = null;

            foreach (L2Recipe r in player._recipeBook)
            {
                if (r.RecipeID == _id)
                {
                    rec = r;
                    break;
                }
            }

            if (rec == null)
            {
                player.sendSystemMessage(SystemMessage.SystemMessageId.RECIPE_INCORRECT);
                player.sendActionFailed();
                return;
            }

            player.unregisterRecipe(rec, true);
        }
    }
}