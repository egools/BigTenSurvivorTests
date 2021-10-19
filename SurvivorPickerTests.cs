using FluentAssertions;
using GoolsDev.Functions.FantasyFootball.Models.BigTenSurvivor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigTenSurvivorTests
{
    [TestFixture]
    public class SurvivorPickerTests
    {
        [Test]
        public void CheckPicks_AllCorrectPicks_PlayerNoEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 1,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week3Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 4,
                        Team = "Week4Pick"
                    }
                }
            };

            picker.CheckPicks(1, 4);

            picker.Eliminated.Should().BeFalse();
            picker.EliminationReason.Should().BeNull();
            picker.WeekEliminated.Should().BeNull();
        }

        [Test]
        public void CheckPicks_AllCorrectPicks_LateStart_PlayerNoEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week3Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 4,
                        Team = "Week4Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 5,
                        Team = "Week5Pick"
                    }
                }
            };

            picker.CheckPicks(2, 5);

            picker.Eliminated.Should().BeFalse();
            picker.EliminationReason.Should().BeNull();
            picker.WeekEliminated.Should().BeNull();
        }

        [Test]
        public void CheckPicks_MissingWeek_PlayerEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 1,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week3Pick"
                    }
                }
            };

            picker.CheckPicks(1, 4);

            picker.Eliminated.Should().BeTrue();
            picker.EliminationReason.Should().BeEquivalentTo("No Pick");
            picker.WeekEliminated.Should().Be(4);
        }

        [Test]
        public void CheckPicks_MissingEarlierWeek_PlayerEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 4,
                        Team = "Week3Pick"
                    }
                }
            };

            picker.CheckPicks(1, 4);

            picker.Eliminated.Should().BeTrue();
            picker.EliminationReason.Should().BeEquivalentTo("No Pick");
            picker.WeekEliminated.Should().Be(1);
        }

        [Test]
        public void CheckPicks_RepeatPick_PlayerEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 1,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week3Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 4,
                        Team = "Week1Pick"
                    }
                }
            };

            picker.CheckPicks(1, 4);

            picker.Eliminated.Should().BeTrue();
            picker.EliminationReason.Should().BeEquivalentTo("Repeat Pick: Week1Pick");
            picker.WeekEliminated.Should().Be(4);
        }

        [Test]
        public void CheckPicks_IncorrectPick_PlayerEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 1,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = false,
                        Week = 3,
                        Team = "Week3Pick"
                    }
                }
            };

            picker.CheckPicks(1, 3);

            picker.Eliminated.Should().BeTrue();
            picker.EliminationReason.Should().BeEquivalentTo("Incorrect Pick: Week3Pick");
            picker.WeekEliminated.Should().Be(3);
        }

        [Test]
        public void CheckPicks_IncorrectPick_PlayerKeptPicking_PlayerEliminated()
        {
            var picker = new SurvivorPicker
            {
                Eliminated = false,
                EliminationReason = null,
                WeekEliminated = null,
                Picks = new List<SurvivorSelection>
                {
                    new SurvivorSelection
                    {
                        Correct = false,
                        Week = 1,
                        Team = "Week1Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 2,
                        Team = "Week2Pick"
                    },
                    new SurvivorSelection
                    {
                        Correct = true,
                        Week = 3,
                        Team = "Week3Pick"
                    }
                }
            };

            picker.CheckPicks(1, 3);

            picker.Eliminated.Should().BeTrue();
            picker.EliminationReason.Should().BeEquivalentTo("Incorrect Pick: Week1Pick");
            picker.WeekEliminated.Should().Be(1);
        }
    }
}