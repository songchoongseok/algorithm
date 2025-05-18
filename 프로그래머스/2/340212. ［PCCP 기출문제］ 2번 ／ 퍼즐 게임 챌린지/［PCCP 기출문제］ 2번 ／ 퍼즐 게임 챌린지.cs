using System;
using System.Linq;
using System.IO;

public class Solution {
    public int solution(int[] diffs, int[] times, long limit) {
        int answer = 0;
        
        int minLevel = 1;
        int maxLevel = diffs.Max(x => x);
        while(minLevel < maxLevel)
        {
            //Console.WriteLine($"{minLevel}, {maxLevel}");
            
            var midLevel = (minLevel + maxLevel) / 2;
            var exTime = Solve(diffs, times, midLevel);
            
            if(exTime > limit)
            {
                minLevel = midLevel + 1;
            }
            else
            {
                maxLevel = midLevel;
            }
            
        }
            
        return minLevel;
    }
    
    public long Solve(int[] diffs, int[] times, int level)
    {
        long time = 0;
        
        time += GetSolveTime(diffs[0], 0, times[0], level);
        for(int i=1; i<diffs.Length; i++)
        {
            time += GetSolveTime(diffs[i], times[i-1], times[i], level);
        }
        
        return time;
    }
    
    private long GetSolveTime(int diff, int time_prev, int time, int level)
    {
        return diff <= level 
            ? time
            : (diff - level) * (time + time_prev) + time;
    }
}