# TimerPerfTests
Testing different ways to do ms-range repeated / delayed actions in C#

## Timer types
### Periodic  
A repeating action that happens at <period> intervals, that can be started or stopped    
Requirements:  
1. Minimal CPU overhead for scheduling the action
1. Accuracy of interval  
(Ideally measured between fire points, not between end of last fire and beginning of next)
1. As low an interval as possible
Ideally below minimum system timer granularity (~15ms) - 1ms would be perfect  

### Timeout
A delayed action that happens once, at `<now> + <period>`  
Can be restarted while running (move currently running timer to `<now> + <period>`)  
Requirements:  
1. Minimal CPU overhead to start / stop / restart  
1. Expected rate of calling of restart is up to 1khz  

This quest is as much about nice, modern code as it is about performance, but not to a point where too much performance is sacrificed
