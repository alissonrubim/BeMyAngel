using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Config
{
    internal static class TestUsers
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
                /*List of Testable Psychiatrist*/
                new TestUser {
                    SubjectId = "C5F1E2E8-195F-4FB3-BDFC-F9FBC51BBD61",
                    Username = "psy1@testuser.com",
                    Password = "1234",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "psy1@testuser.com"),
                        new Claim(JwtClaimTypes.Role, IdentityResources.Psychiatrist)
                    }
                },
                new TestUser {
                    SubjectId = "ECC0F0B4-9340-48A2-84D5-DB2A7F9C7023",
                    Username = "psy2@testuser.com",
                    Password = "1234",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "psy2@testuser.com"),
                        new Claim(JwtClaimTypes.Role, IdentityResources.Psychiatrist)
                    }
                },
                /*List of Testable Patients*/
                new TestUser {
                    SubjectId = "1F374696-77A8-422D-9996-046950E79727",
                    Username = "pat1@testuser.com",
                    Password = "1234",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "pat1@testuser.com"),
                        new Claim(JwtClaimTypes.Role, IdentityResources.Patient)
                    }
                },
                new TestUser {
                    SubjectId = "03ECDD23-A865-44C8-B28F-033DD0E6DD62",
                    Username = "pat2@testuser.com",
                    Password = "1234",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Email, "pat2@testuser.com"),
                        new Claim(JwtClaimTypes.Role, IdentityResources.Patient)
                    }
                }
            };
        }
    }
}
