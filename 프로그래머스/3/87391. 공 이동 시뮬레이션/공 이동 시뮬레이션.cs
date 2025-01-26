using System;
using System.IO;
public class Solution {
    public long solution(int n, int m, int x, int y, int[,] queries) {
        (long s, long e) row = (x,x);
        (long s, long e) col = (y,y);
        
        int cnt = queries.GetLength(0) - 1;
        for(int i=cnt; i>=0; i--)
        {
            int type = queries[i,0];
            int dist = queries[i,1];
            switch(type)
            {
                case 0:
                    col.s = col.s != 0 ? col.s + dist : 0;
                    col.e = col.e + dist > m - 1 ? m - 1 : col.e + dist;
                    break;
                case 1:
                    col.s = col.s - dist < 0 ? 0 : col.s - dist;
                    col.e = col.e != m - 1 ? col.e - dist : m - 1;
                    break;
                case 2:
                    row.s = row.s != 0 ? row.s + dist : 0;
                    row.e = row.e + dist > n - 1 ? n - 1 : row.e + dist;
                    break;
                case 3:
                    row.s = row.s - dist < 0 ? 0 : row.s - dist;
                    row.e = row.e != n - 1 ? row.e - dist : n - 1;
                    break;
            }
            
            if(row.s > n-1 || col.s > m-1 || row.e < 0 || col.e < 0)
            return 0;
        }
        
        
        
        long v = (Math.Abs(row.e - row.s) + 1);
        long w = (Math.Abs(col.e - col.s) + 1);
        return (v * w);
    }
}