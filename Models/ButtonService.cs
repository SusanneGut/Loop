//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Loop.Models.Entities;
//using Loop.Models.ViewModels;
//using Microsoft.EntityFrameworkCore;

//namespace Loop.Models
//{
//    public class ButtonService
//    {
//        private readonly LoopContext context;

//        public ButtonService(LoopContext context)
//        {
//            this.context = context;
//        }

//        public async Task<ButtonIndexVM[]> GetAllTimes()
//        {

//			return await context
//				.Timestamp
//				.Select(o => new ButtonIndexVM
//				{
//					Start = o.Start,
//					Stop = o.Stop,
//					Span = Convert.ToDateTime(o.Stop)-Convert.ToDateTime(o.Start)
//                })
//                .ToArrayAsync();
//        }

//        //public async Task<ButtonIndexVM> GetTimeByProject(int id)
//        //{
//        //	return await context
//        //		.Timestamp
//        //		.Select(o=> new ButtonIndexVM
//        //		{
//        //			ActivityName = o.Activity.ActivityName,
//        //			ActivityId = o.Id,
//        //			Start = o.Start,
//        //			Stop = o.Stop
//        //		})
//        //		.SingleOrDefaultAsync(e => e.ActivityId == id);
//        //}


//        public async Task SetStart(string time, MemberActivitiesVM activity)
//        {
//            if(context.Timestamp.Count() > 0)
//            {
//                var lastPost = context
//                    .Timestamp
//                    .Last();

//                if(lastPost.Stop != null)
//                {
//                    await context
//                        .Timestamp
//                        .AddAsync(new Timestamp
//                        {
//                            Start = time,
//                            ActivityId = activity

//                        });

//                    await context.SaveChangesAsync();
//                }
//            }
//            else
//            {
//                await context
//                    .Timestamp
//                    .AddAsync(new Timestamp
//                    {
//                        Start = time,
//                        ActivityId = activity.Id
//                    });
//                await context.SaveChangesAsync();
//            }
//        }

//        public async Task SetStop(string time)
//        {
//            if(context.Timestamp.Count() > 0)
//            {
//                var lastPost = context
//                    .Timestamp
//                    .Last();
//                if(lastPost.Stop == null)
//                {
//                    lastPost.Stop = time;
//                }

//                await context.SaveChangesAsync();
//            }
//        }
//    }
//}
