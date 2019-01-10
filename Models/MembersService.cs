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
			var activeStatus = context.Timestamp.LastOrDefault().Stop;

			
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
			if (activeStatus == null)
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
			//var activeStatus = context.Timestamp.Where(o => o.ActivityId == id).LastOrDefault().Stop;

			//Total time
			foreach (var item in listOfTimes)
			{
				var rowTotal = Convert.ToDateTime(item.Stop) - Convert.ToDateTime(item.Start);
				totalTime = totalTime.Add(rowTotal);

			}

			if (!isEmpty)
			{
                lastStart = Convert.ToDateTime(listOfTimes.LastOrDefault().Start);
                lastStop = DateTime.UtcNow;

                //DateTime lastPostStart = Convert.ToDateTime(listOfTimes.LastOrDefault().Start);
                //DateTime lastPostStop = Convert.ToDateTime(listOfTimes.LastOrDefault().Stop);
                //lastStop = lastPostStop;
                //lastStart = lastPostStart;

				//Om .stop är null => .stop isActive.
                var stopStatus = listOfTimes.LastOrDefault().Stop;

                if(stopStatus == null)
                {
					isActive = true;
                    lastStop = Convert.ToDateTime(listOfTimes.LastOrDefault().Stop);
				}
                //totalTime = TimeSpan.Parse(listOfTimes.LastOrDefault().TotalTime);
			}

			return await context
			.Activity
			.Where(i => i.Id == id)
			.Select(o => new MemberActivityVM
			{
                    //TotalTime = totalTime,
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
            //string totalTime = TimeSpan.Zero.ToString();

            if(isEmpty || (!isEmpty && selectedActivityList.Last().Stop != null))
            {
                await context
                    .Timestamp
                    .AddAsync(new Timestamp
                    {
                        //TotalTime = (DateTime.UtcNow - Convert.ToDateTime(selectedActivityList.LastOrDefault().Start)).ToString(),
                        Start = startTime,
                        ActivityId = id
                    });
                await context.SaveChangesAsync();
            }
        }

        public async Task SetStop(string stopTime, int id)
        {
            bool isEmpty = !context.Timestamp.Where(o => o.ActivityId == id).Any();
            int listOfTimesLength = context.Timestamp.Where(o => o.ActivityId == id).Count();

            Timestamp lastRow;
            Timestamp secondToLastRow;
            TimeSpan totalTime;
            TimeSpan totalRowTime;

            if(!isEmpty)
            {
                lastRow = context.Timestamp.Where(o => o.ActivityId == id).Last();

                if(lastRow.Stop == null)
                {
                    if(listOfTimesLength == 1)
                    {
                        lastRow.Stop = stopTime;
                        totalRowTime = Convert.ToDateTime(lastRow.Stop) - Convert.ToDateTime(lastRow.Start);
                        lastRow.TotalTime = totalRowTime.ToString();
                    }

                    if(listOfTimesLength > 1)
                    {
                        lastRow.Stop = stopTime;
                        totalRowTime = Convert.ToDateTime(lastRow.Stop) - Convert.ToDateTime(lastRow.Start);
                        secondToLastRow = context.Timestamp.Where(o => o.ActivityId == id).OrderByDescending(p => p.Id).Skip(1).Take(1).Last();
                        totalTime = TimeSpan.Parse(secondToLastRow.TotalTime) + totalRowTime;
                        lastRow.TotalTime = totalTime.ToString();
                    }
                }
				await context.SaveChangesAsync();
			}
		}
	}
}

