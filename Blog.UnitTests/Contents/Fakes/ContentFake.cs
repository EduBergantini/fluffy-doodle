﻿using System.Collections.Generic;

using Bogus;

using Blog.Domain.Contents.Entities;


namespace Blog.UnitTests.Contents.Fakes
{
    public class ContentFake
    {
        private Faker faker;

        private ContentFake()
        {
            this.faker = new Faker();
        }

        private Content GetFakeContent()
        {

            return new Content
            {
                Id = this.faker.Random.Int(min: 0),
                FeaturedImage = this.faker.Internet.Avatar(),
                Title = this.faker.Lorem.Sentence(),
                Subtitle = this.faker.Lorem.Paragraph()
            };
        }

        public static IEnumerable<Content> GetContentList(int numberOfContents = 3)
        {
            var contentFake = new ContentFake();
            var contents = new List<Content>();
            
            for (int i = 0; i < numberOfContents; i++)
            {
                contents.Add(contentFake.GetFakeContent());
            }

            return contents;
        }
    }
}
