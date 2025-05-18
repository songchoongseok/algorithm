using System;
using System.IO;

public class Solution {
    public string solution(string video_len, string pos, string op_start, string op_end, string[] commands) {
        var totalTime = ToSeconds(video_len);
        var posTime = ToSeconds(pos);
        var opStartTime = ToSeconds(op_start);
        var opEndTime = ToSeconds(op_end);
        
        var ans = posTime;
        foreach(var command in commands)
        {
            ans = SkipOp(ans, opStartTime, opEndTime);
            ans = Execute(command, totalTime, ans, opStartTime, opEndTime);
            ans = SkipOp(ans, opStartTime, opEndTime);
            
        }
        
        return ToTime(ans);
    }
    
    public string ToTime(int time)
    {
        var min = time / 60;
        var sec = time % 60;
        
        return $"{min.ToString("00")}:{sec.ToString("00")}";
    }
    
    public int ToSeconds(string len)
    {
        var min = Convert.ToInt32(len.Split(":")[0]);
        var sec = Convert.ToInt32(len.Split(":")[1]);
        
        return min * 60 + sec;
    }
    
    public int Execute(string command, int total, int pos, int opStart, int opEnd)
    {
        switch(command)
        {
            case "prev":
                return pos - 10 < 0 ? 0 : pos - 10;
            case "next":
                return pos + 10 > total ? total : pos + 10;
        }
        
        return -1;
    }
    
    public int SkipOp(int pos, int opStart, int opEnd)
    {
        return (opStart <= pos && pos <= opEnd) ? opEnd : pos;
    }
}