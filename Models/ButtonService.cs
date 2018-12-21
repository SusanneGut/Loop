using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Loop.Models
{
    public class ButtonService
    {
        private readonly LoopContext context;

        public ButtonService(LoopContext context)
        {
            this.context = context;
        }

        public async Task<ButtonIndexVM[]> GetAllTimes()
        {
            return await context
                .Timestamp
                .Select(o => new ButtonIndexVM
                {
                    Start = o.Start,
                    Stop = o.Stop
                })
                .ToArrayAsync();
        }

        public async Task SetStart(string time)
        {
            await context
                 .Timestamp
                 .AddAsync(new Timestamp
                 {
                     Start = time
                 });
            await context.SaveChangesAsync();
        }

        public async Task SetStop(string time)
        {
            var lastPostId = context
                .Timestamp
                .Select(o => o.Id)
                .Max();

            var test = context
                .Timestamp
                .Last();

            if(test.Start != null)
            {
                await context
                .Timestamp
                //.Where(o=>o.Id==lastPostId)
                .AddAsync(new Timestamp
                {
                    Stop = time
                });
                await context.SaveChangesAsync();
            }
        }


    }
}
