﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CtsContestWeb.Db.Repository;
using CtsContestWeb.Dto;
using CtsContestWeb.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtsContestWeb.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IBalanceLogic _balanceLogic;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IBalanceLogic balanceLogic, IPurchaseRepository purchaseRepository, IUserRepository userRepository)
        {
            _balanceLogic = balanceLogic;
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public UserInfoDto GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                _userRepository.InsertIfNotExists(User);
                return new UserInfoDto
                {
                    Email = User.FindFirst(ClaimTypes.Email).Value,
                    Name = User.FindFirst(ClaimTypes.GivenName).Value,
                    Balance = _balanceLogic.GetCurrentBalance(User.FindFirst(ClaimTypes.Email).Value),
                    IsLoggedIn = true
                };
            }

            return new UserInfoDto
            {
                IsLoggedIn = false
            };
        }

        [HttpGet("purchases")]
        public List<PurchaseDto> GetUserPurchases()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.FindFirst(ClaimTypes.Email).Value;
                var purchases = _purchaseRepository.GetAllByUserEmail(userEmail);

                return purchases.ToList().Select(p => new PurchaseDto
                {
                    PrizeId = p.PrizeId,
                    Price = p.Cost,
                    IsGivenAway = p.GivenPurchase != null,
                    PurchaseId = p.PurchaseId,
                    UserEmail = userEmail
                }).ToList();
            }

            return new List<PurchaseDto>();
        }
    }
}