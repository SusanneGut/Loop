using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Loop.Models
{
    public class MembersService
    {
        LoopContext context;
        readonly UserManager<IdentityUser> userManager;


        public MembersService(LoopContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task AddActivity(MemberCreateActivityVM activity, string id)
        {
            await context
                .Activity
                .AddAsync(new Activity
                {
                    ActivityName = activity.ActivityName,
                    UserId = id
                });

            await context.SaveChangesAsync();
        }


        public async Task<MemberActivitiesVM> GetAllActivities(string id)
        {
            return new MemberActivitiesVM
            {
                Activities = await context
                .Activity
                .Where(e => e.UserId == id)
                .OrderBy(o => o.ActivityName)
                .Select(a => new MemberActivityVM
                {
                    ActivityName = a.ActivityName,
                    ActivityId = a.Id

                })
                .ToArrayAsync()
            };

        }

        public bool GetActiveStatus(int id)
        {
            bool isActive = false;
            var activeStatus = context.Timestamp.Where(o => o.ActivityId == id).LastOrDefault().Stop;
            if(activeStatus == null)
            {
                isActive = true;
            }
            return isActive;
        }

        public async Task<MemberActivityVM> GetActivityById(int id)
        {
            var listOfTimes = context.Timestamp.Where(o => o.ActivityId == id);
            TimeSpan currentTime = TimeSpan.Zero;
            TimeSpan totalTime = TimeSpan.Zero;
            DateTime lastStop;
            DateTime lastStart;
            bool isActive = false;
            bool isEmpty = !listOfTimes.Any();

            //Total time
            foreach(var item in listOfTimes)
            {
                var rowTotal = Convert.ToDateTime(item.Stop) - Convert.ToDateTime(item.Start);
                totalTime = totalTime.Add(rowTotal);
            }

            if(!isEmpty)
            {
                var lastPostStart = Convert.ToDateTime(listOfTimes.LastOrDefault().Start);
                var lastPostStop = Convert.ToDateTime(listOfTimes.LastOrDefault().Stop);
                lastStop = lastPostStop;
                lastStart = lastPostStart;

                //Om .stop är null => .stop isActive.
                var stopStatus = context
                    .Timestamp
                    .Where(o => o.ActivityId == id)
                    .LastOrDefault()
                    .Stop;
                if(stopStatus == null)
                {
                    currentTime = DateTime.UtcNow - lastPostStart;
                    isActive = true;
                }
                else
                    currentTime = lastStop - lastStart;


            }

            return await context
                .Activity
                .Where(i => i.Id == id)
                .Select(o => new MemberActivityVM
                {
                    Span = currentTime,
                    //Span = totalTime,
                    ActivityId = o.Id,
                    ActivityName = o.ActivityName,
                    IsActive = isActive,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<MemberEditActivityVM> GetActivityEditAsync(int id)
        {
            return await context
                .Activity
                .Select(o => new MemberEditActivityVM
                {
                    ActivityName = o.ActivityName,
                    Id = o.Id,

                })
                .SingleOrDefaultAsync(e => e.Id == id);
        }


        //public async Task<MemberEditVM> GetUserByNameAsync(string user)
        //{
        //    var identityUser = await userManager.FindByNameAsync(user);

        //    return new MemberEditVM
        //    {
        //        Name = identityUser.UserName,
        //        Email = identityUser.Email,
        //        OldName = identityUser.UserName
        //    };

        //}

        //public async Task EditAsync(MemberEditVM User)
        //{
        //    var user = await userManager.FindByNameAsync(User.OldName);

        //    await userManager.SetUserNameAsync(user, User.Name);
        //    await userManager.SetEmailAsync(user, User.Email);

        //    await userManager.UpdateAsync(user);
        //    await context.SaveChangesAsync();

        //}

        public async Task EditActivityAsync(MemberEditActivityVM input)
        {
            var a = await context.Activity.FindAsync(input.Id);

            a.ActivityName = input.ActivityName;

            await context.SaveChangesAsync();
        }

        public async Task SetStart(string startTime, int id)
        {
            bool isEmpty = !context.Timestamp.Where(o => o.ActivityId == id).Any();
            var selectedActivityList = context.Timestamp.Where(o => o.ActivityId == id);

            if(isEmpty || (!isEmpty && selectedActivityList.Last().Stop != null))
            {
                await context
                    .Timestamp
                    .AddAsync(new Timestamp
                    {
                        Start = startTime,
                        ActivityId = id
                    });
                await context.SaveChangesAsync();
            }
        }

        public async Task SetStop(string stopTime, int id)
        {
            bool isEmpty = !context.Timestamp.Where(o => o.ActivityId == id).Any();
            Timestamp lastRow = context.Timestamp.Where(o => o.ActivityId == id).Last();
            Timestamp secondToLastRow =  context.Timestamp.Where(o => o.ActivityId == id).OrderByDescending(p => p.Id).Skip(1).Take(1).Last();

            if(!isEmpty)
            {
                if(lastRow.Stop == null)
                {
                    lastRow.Stop = stopTime;
                    TimeSpan totalRowTime = Convert.ToDateTime(lastRow.Stop) - Convert.ToDateTime(lastRow.Start);
                    DateTime totalTime = Convert.ToDateTime(secondToLastRow.TotalTime) + totalRowTime;
                    lastRow.TotalTime = totalTime.ToString();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}

