# Sprint 4

## Definition of Ready
1. All team members understand the sprint goals and tasks associated with the User Stories.  
2. User stories are fully refined and estimated, with acceptance criteria clearly defined.  
3. Design mockups for the optimal move suggestion interface, social media sharing buttons, and volume slider are finalized and approved.  
4. Behavioral rules for level saving (US22) are understood by the team.  
5. Gameplay mechanics for point calculation (US11) are defined, including factors like move count, completion time, and difficulty.  
6. Task assignments and subtasks are defined and documented in the project management tool.  
7. Necessary integrations with external libraries or APIs (e.g., social media sharing) are identified and setup instructions are available.  

---

## Definition of Done
1. Levels are saved so that re-entering a level retains its previous state (US22).  
   - The current level state is stored and restored when exiting and re-entering the level.  
   - Previous level states are overwritten only upon successful completion of the level.  
2. Optimal move suggestion functionality is implemented (US15):  
   - The optimal move sequence for a level is calculated and stored at the start of the level.  
   - The sequence is displayed only when the player requests a hint.  
3. Social media sharing functionality is implemented (US14):  
   - Buttons for sharing scores are available and functional for supported platforms.  
   - Past and present scores can be shared with dynamic, user-customized messages.  
4. The volume slider in the pause menu adjusts all in-game audio (US12).  
5. The point system is implemented (US11):  
   - Players earn points based on moves, time, and difficulty.  
   - Points are accurately calculated and displayed upon level completion.  
6. Visual and audio feedback for implemented features (e.g., score sharing, optimal moves) are completed and tested.  
7. Code for all implemented features is committed, peer-reviewed, and merged into the main branch.  
8. The sprint goals are achieved, and the work is approved by the Product Owner.  

---