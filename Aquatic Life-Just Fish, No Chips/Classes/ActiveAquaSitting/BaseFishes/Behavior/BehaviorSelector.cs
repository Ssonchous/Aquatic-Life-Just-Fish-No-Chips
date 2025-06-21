using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.Decorators;
using Aquatic_Life_Just_Fish__No_Chips.Classes.AquaSitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior
{
    public static class BehaviorSelector
    {
        private static void LeaveSchoolFish(BaseFishDecorator fishAsDecorator)
        {
            if (fishAsDecorator != null)
            {
                var schoolingFish = fishAsDecorator.GetDecorator<SchoolingDecorator>();
                if (schoolingFish != null && fishAsDecorator.СurrentBehavior is SchoolingBehavior)
                    schoolingFish.LeaveSchool();
            }
        }
        public static IBehavior Choose(BaseFish fish, AquariumContent content)
        {
            BaseFishDecorator fishAsDecorator = fish as BaseFishDecorator;

            //if (fish is BaseFish baseFish)
            //{
            //    // Пытаемся получить декораторы (если они есть)
            //    var hunter = baseFish.GetDecorator<HunterDecorator>();
            //    var scеhooling = baseFish.GetDecorator<SchoolingDecorator>();

            //    if (hunter != null)
            //        MessageBox.Show($"{baseFish.Name} — охотник (сила укуса: {hunter.BiteStrength})");

            //    if (scеhooling != null)
            //        MessageBox.Show($"{baseFish.Name} — стайная рыба");

            //    if (hunter == null && scеhooling == null)
            //        MessageBox.Show($"{baseFish.Name} — обычная рыба без декораторов");
            //}

            // 1. Проверка голода
            if (fish.Hunger < 70)
            {
                LeaveSchoolFish(fishAsDecorator);

                Food food = content.FindNearestFood(fish);
                if (food != null) return new SeekingFoodBehavior(food, content.MaxPosition);

            // 2. Охота 
                var hunter = fishAsDecorator?.GetDecorator<HunterDecorator>();
                if (hunter != null && hunter.Hunger < 50)
                {
                   // MessageBox.Show("Может охотиться " + fish.Name);
                    BaseFish prey = content.FindNearestPrey(hunter);
                    if (prey != null) {
                        //MessageBox.Show("Врое как охотится " + fish.Name);
                        return new HuntingBehavior(prey, content.MaxPosition); }
                }
            }


            //3. Проверка угроз
            BaseFish hunterDanger = content.FindNearestHunter(fish);
            if (hunterDanger != null)
            {
                LeaveSchoolFish(fishAsDecorator);
                //MessageBox.Show("Опья пасность"+ hunterDanger.Name);
                return new FearsBehavior(hunterDanger, content.MaxPosition);
            }

            // 4. Стайное поведение
            var schooling = fishAsDecorator?.GetDecorator<SchoolingDecorator>();
            if (schooling != null)
            {

                if (fish.СurrentBehavior is SchoolingBehavior && schooling.CheckСorrectSchool())
                {
                    return fish.СurrentBehavior; }
                else
                {
                    List<BaseFish> listFishBro = content.FindNearestListFishBro(schooling);
                    if (listFishBro.Count > 0)
                        foreach (BaseFish potential in listFishBro)
                        {
                            var potentialLeader = (potential as BaseFishDecorator)?.GetDecorator<SchoolingDecorator>();
                            if (potentialLeader != null && schooling.TryJoinSchool(potentialLeader))
                            {
                                return new SchoolingBehavior(content.MaxPosition);
                            }
                        }

                }               
            }

            // 5. Игра с пузырями
            Bubble bubble = content.FindNearestBubble(fish);
            if (fish.СurrentBehavior is PlayingBehavior)
                return fish.СurrentBehavior;
            if (bubble != null) return new PlayingBehavior(bubble, content.MaxPosition);

            // 5. Дефолтное поведение
            return new IdleBehavior(content.MaxPosition);
        }
    }
}
