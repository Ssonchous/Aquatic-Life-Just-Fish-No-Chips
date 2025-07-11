﻿using Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Aquatic_Life_Just_Fish__No_Chips.Classes.ActiveAquaSitting.BaseFishes.Decorators
{
    public class SchoolingDecorator : BaseFishDecorator
    {
        public SchoolingDecorator Leader { get; private set; }
        public bool IsLeader { get; private set; }
        private int schoolsCount;
        private int MaxSchoolSize;
        private double deviationAngle;


        public SchoolingDecorator(BaseFish fish, int maxSchoolSize) : base(fish)
        {
            deviationAngle = new Random().NextDouble() * 0.2 - 0.1;
            schoolsCount = 0;
            MaxSchoolSize = maxSchoolSize;

        }

        //public bool TryJoinSchool(SchoolingDecorator potentialLeader)
        //{
        //    var currentLeader = potentialLeader;
        //    while (!currentLeader.Leader != null || currentLeader.Leader != this)
        //    {
        //        currentLeader = currentLeader.Leader;

        //        if (currentLeader == potentialLeader)
        //            return false;
        //    }

        //    // Проверяем, не пытаемся ли присоединиться к себе
        //    if (currentLeader == this)
        //        return false;

        //    // Проверяем размер стаи
        //    if (currentLeader.schoolsCount >= MaxSchoolSize)
        //        return false;

        //    // Устанавливаем лидера
        //    Leader = currentLeader;
        //    Leader.schoolsCount++;
        //    return true;
        //}

        public bool TryJoinSchool(SchoolingDecorator potentialLeader)
        {
            if (potentialLeader == this)
                return false;

            if (this.IsLeader)
                return true;

            if (potentialLeader.Leader != null)
                return TryJoinSchool(potentialLeader.Leader);

            if (!(potentialLeader.СurrentBehavior is HuntingBehavior) && !(potentialLeader.СurrentBehavior is SeekingFoodBehavior) && !(potentialLeader.СurrentBehavior is FearsBehavior) )
            {
                if (potentialLeader.IsLeader)
                    if (potentialLeader.schoolsCount >= MaxSchoolSize)
                        return false;
                    else { }
                else if (potentialLeader.schoolsCount == 0)
                {
                    potentialLeader.IsLeader = true;
                    potentialLeader.Leader = null;
                }

                this.Leader = potentialLeader;
                potentialLeader.schoolsCount++;

                return true;
            }
            else
                return false;

        }



        public bool CheckСorrectSchool()
        {
            if (IsLeader && schoolsCount > 0)
                return true;
            if (Leader != null && Leader.IsLeader)
                return true;
            return false;
        }

        public void LeaveSchool()
        {
            if (Leader != null)
            {
                Leader.schoolsCount--;
                Leader = null;
            }
            IsLeader = false;
            schoolsCount = 0;
        }

        public double GetSchoolingAngle()
        {
            if (Leader != null)
            {
                return Leader.CurrentAngle + deviationAngle;
            }
            else return this.CurrentAngle;
        }

    }
}
