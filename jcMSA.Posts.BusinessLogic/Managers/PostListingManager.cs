﻿using System;
using System.Collections.Generic;
using System.Linq;

using jcMSA.Common.PCL.Transports.Container;
using jcMSA.Common.WebAPI;
using jcMSA.Posts.DataLayer.Entities;
using jcMSA.Posts.PCL.Transports;

namespace jcMSA.Posts.BusinessLogic.Managers {
    public class PostListingManager : BaseManager {
        public ReturnSet<List<PostListingResponseItem>> GetPostListing() {
            using (var eFactory = new EFModel()) {
                var postListing = eFactory.Database.SqlQuery<PostListing>("POSTS_getPostListingSP");

                if (postListing == null) {
                    throw new Exception("SP in getting posts returned null");
                }

                return new ReturnSet<List<PostListingResponseItem>>(postListing.Select(a => new PostListingResponseItem {
                    PostDate = a.Created.DateTime, Title = a.Title, URL = a.SafeURL
                }).ToList());
            }
        }
    }
}