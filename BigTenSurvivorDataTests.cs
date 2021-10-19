using FluentAssertions;
using GoolsDev.Functions.FantasyFootball.Models.BigTenSurvivor;
using NUnit.Framework;
using System.Collections.Generic;

namespace BigTenSurvivorTests
{
    [TestFixture]
    public class BigTenSurvivorDataTests
    {
        [Test]
        public void WeekPicksCompleted_AllPickersHaveCurrentWeek_ReturnsTrue()
        {
            var sut = new BigTenSurvivorData
            {
                Pickers = new List<SurvivorPicker>
                {
                    new SurvivorPicker
                    {
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 1,
                                Correct = true
                            }
                        }
                    },
                    new SurvivorPicker
                    {
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 1,
                                Correct = true
                            }
                        }
                    }
                }
            };

            var actual = sut.AllWeekPicksCompleted(1);

            actual.Should().BeTrue();
        }

        [Test]
        public void WeekPicksCompleted_PickersIsMissingCurrentWeek_ReturnsFalse()
        {
            var sut = new BigTenSurvivorData
            {
                Pickers = new List<SurvivorPicker>
                {
                    new SurvivorPicker
                    {
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 1,
                                Correct = true
                            }
                        }
                    },
                    new SurvivorPicker
                    {
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 2,
                                Correct = true
                            }
                        }
                    }
                }
            };

            var actual = sut.AllWeekPicksCompleted(2);

            actual.Should().BeFalse();
        }

        [Test]
        public void WeekPicksCompleted_AllNonEliminatedPickersHaveCurrentWeek_ReturnsTrue()
        {
            var sut = new BigTenSurvivorData
            {
                Pickers = new List<SurvivorPicker>
                {
                    new SurvivorPicker
                    {
                        Eliminated = true,
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 1,
                                Correct = false
                            }
                        }
                    },
                    new SurvivorPicker
                    {
                        Picks = new List<SurvivorSelection>
                        {
                            new SurvivorSelection
                            {
                                Week = 2,
                                Correct = true
                            }
                        }
                    }
                }
            };

            var actual = sut.AllWeekPicksCompleted(2);

            actual.Should().BeTrue();
        }
    }
}